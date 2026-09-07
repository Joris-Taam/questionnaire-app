import { CommonModule } from '@angular/common';
import { Component, EventEmitter, input, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() btnText: string = '';
  @Input() btnIcon: string = '';
  @Input() btnType: string = '';
  @Input() btnCustomClass: string = '';
  @Input() btnTextAlign: string = '';
  @Input() btnTextColor: string = '';
  @Input() btnBackgroundColor: string = '';
  @Input() btnLeftIcon: string = '';
  @Input() btnRightIcon: string = '';
  @Input() disabled: boolean = false;


  @Output() saveClicked = new EventEmitter<void>();
  @Output() editClicked = new EventEmitter<void>();
  @Output() deleteClicked = new EventEmitter<void>();

  ngOnInit(): void { }

  get btnTypeClass(): string {
    return this.btnType ? 'btn-' + this.btnType.toLowerCase() : '';
  }

  get btnIconClass(): string {
    return this.btnIcon ? 'fa-' + this.btnIcon.toLowerCase() : '';
  }

  get btnTextClass(): string {
    return this.btnTextColor ? 'text-' + this.btnTextColor.toLowerCase() : '';
  }

  get btnTextAlignClass(): string {
    return this.btnTextAlign ? 'text-' + this.btnTextAlign.toLowerCase() : '';
  }

  get btnLeftIconClass(): string {
    return this.btnLeftIcon ? 'fa-' + this.btnLeftIcon.toLowerCase() : '';
  }

  get btnRightIconClass(): string {
    return this.btnRightIcon ? 'fa-' + this.btnRightIcon.toLowerCase() : '';
  }
}
