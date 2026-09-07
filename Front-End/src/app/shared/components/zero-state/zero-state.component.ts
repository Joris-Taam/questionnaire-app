import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-zero-state',
  templateUrl: './zero-state.component.html',
  styleUrls: ['./zero-state.component.scss'],
})
export class ZeroStateComponent {
  @Input() mainText: string = 'Geen resultaten gevonden';
  @Input() subText: string = 'Er zijn geen vragenlijsten gevonden';
  @Input() icon: string = 'fas fa-search';
}
