import { Injectable, inject } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Footerservice {
  #buttonVisibilitySubject = new BehaviorSubject<{ showToevoegen: boolean; showWijzigen: boolean; showDelete: boolean }>({
    showToevoegen: false,
    showWijzigen: false,
    showDelete: false,
  });

  buttonVisibility$ = this.#buttonVisibilitySubject.asObservable();
  #router = inject(Router);
  #activatedRoute = inject(ActivatedRoute);
  #id: string | null = '';
  questionId: string | null = '';

  constructor() {
    this.#router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updateButtonVisibility();
      }
    });
  }

  ngOnInit() {
    this.#getIdFromRoute();
    this.#getQuestionFromRoute();
  }

  #getIdFromRoute(): void {
    this.#id = this.#activatedRoute.snapshot.queryParamMap.get('id');
  }

  #getQuestionFromRoute(): void {
    this.questionId = this.#activatedRoute.snapshot.queryParamMap.get('question');
  }

  deleteVisibility(): boolean {
    const isAddPage = this.#router.url.split('?')[0] === '/admin/questionnaireadd';
    const isQuestionPage = this.#router.url.includes("&question=");
    return !isAddPage && !this.questionId && !isQuestionPage;
  }

  public updateButtonVisibility() {
    const path = this.#router.url.split('?')[0];
    const visibility = {
      showToevoegen: path === '/admin/questionnaireadd',
      showWijzigen: path === '/admin/questionnaireedit',
      showDelete: this.deleteVisibility(),
    };

    this.setButtonVisibility(visibility);
  }

  private setButtonVisibility(visibility: { showToevoegen: boolean; showWijzigen: boolean; showDelete: boolean }) {
    this.#buttonVisibilitySubject.next(visibility);
  }

  public getButtonVisibility() {
    return this.#buttonVisibilitySubject.value;
  }
}
