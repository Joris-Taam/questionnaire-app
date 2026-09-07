import { Component, ElementRef, inject, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { HeaderService } from '../../services/header.service';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription, filter } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {
  @ViewChild('mySelect') mySelect!: ElementRef<HTMLSelectElement>;
  #headerService = inject(HeaderService);
  #router = inject(Router);
  selectedValue = '';
  protected resultDetailPage: boolean = false;
  public currentView: string = localStorage.getItem('view') === 'participant' ? 'ParticipantView' : 'GeneralView';
  #viewModeSubscription = new Subscription();
  #resultDetailSubscription = new Subscription();
  #routeSubscription = new Subscription();

  ngOnInit(): void {
    this.#viewModeSubscription = this.#headerService.viewMode$.subscribe((mode) => {
      this.selectedValue = mode === 'participant' ? 'ParticipantView' : 'GeneralView';
    });

    this.#headerService.updateResultDetailStatus();

    this.#routeSubscription = this.#router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.#headerService.updateResultDetailStatus();
      });

    this.#resultDetailSubscription = this.#headerService.resultDetailPage$.subscribe(
      (status) => {
        this.resultDetailPage = status;
      }
    );
  }

  checkDropDownValue() {
    const view = localStorage.getItem('view');
    this.selectedValue = view === 'participant' ? 'ParticipantView' : 'GeneralView';
  }

  changeResultView(view: string): void {
    const mode = view === 'GeneralView' ? 'general' : 'participant';
    localStorage.setItem('view', mode);
    this.#headerService.setViewMode(mode);
  }

  ngOnDestroy(): void {
    this.#resultDetailSubscription.unsubscribe();
    this.#routeSubscription.unsubscribe();
  }
}
