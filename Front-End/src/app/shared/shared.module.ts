import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ListItemComponent } from './components/list-item/list-item.component';
import { ButtonComponent } from './components/button/button.component';
import { ZeroStateComponent } from './components/zero-state/zero-state.component'; 
import { SpinnerComponent } from './components/spinner/spinner.component';

@NgModule({
  declarations: [
    HeaderComponent, 
    FooterComponent,
    ListItemComponent,
    SpinnerComponent
  ],
  imports: [
    CommonModule,
    ButtonComponent,
    ZeroStateComponent,
  ],
  exports: [
    HeaderComponent, 
    FooterComponent,
    ListItemComponent,
    ButtonComponent,
    ZeroStateComponent,
    SpinnerComponent
  ]
})
export class SharedModule { }
