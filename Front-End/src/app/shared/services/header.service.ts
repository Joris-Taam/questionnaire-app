import { inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";

@Injectable({ providedIn: 'root' })

export class HeaderService {
    #initialMode = (localStorage.getItem('view') as 'general' | 'participant') ?? 'general';
    #viewModeSubject = new BehaviorSubject<'general' | 'participant'>(this.#initialMode);
    viewMode$ = this.#viewModeSubject.asObservable();
    #resultDetailSubject = new BehaviorSubject<boolean>(false);
    #router = inject(Router)

    setViewMode(mode: 'general' | 'participant') {
        this.#viewModeSubject.next(mode);
        localStorage.setItem('view', mode);
    }

    public resultDetailRoute(): boolean {
        const path = this.#router.url;
        return path.includes('resultdetail');
    }

    public get resultDetailPage$() {
        return this.#resultDetailSubject.asObservable();
    }

    public updateResultDetailStatus(): void {
        const isResultDetail = this.resultDetailRoute();
        this.#resultDetailSubject.next(isResultDetail);
    }

}
