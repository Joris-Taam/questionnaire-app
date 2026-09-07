import { Component, Input, Output, EventEmitter, OnInit, OnDestroy, inject } from '@angular/core';
import { Footerservice } from '../../services/footer.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})

export class FooterComponent implements OnInit, OnDestroy {
  @Output() saveClicked = new EventEmitter<void>();
  @Output() editClicked = new EventEmitter<void>();
  @Output() deleteClicked = new EventEmitter<void>();
  @Input() isFormValid: boolean = false; 

  #footerService = inject(Footerservice);
  private subscriptions = new Subscription();
  buttonVisibility = {
    showToevoegen: false,
    showWijzigen: false,
    showDelete: false,
  };

  ngOnInit(): void {
    this.subscriptions.add(
      this.#footerService.buttonVisibility$.subscribe(
        (visibility) => {
          this.buttonVisibility = visibility;
        }
      )
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  protected onSaveClick() {
    this.saveClicked.emit();
  }

  protected onEditClick() {
    this.editClicked.emit();
  }
  
  protected onDeleteClick() {
    this.deleteClicked.emit();
  }
}
