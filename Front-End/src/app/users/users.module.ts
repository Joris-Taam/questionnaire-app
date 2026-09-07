import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { LOCALE_ID } from '@angular/core';
import { UsersRoutingModule } from './users-routing.module';

import localeNl from '@angular/common/locales/nl';
import { SharedModule } from '../shared/shared.module';

registerLocaleData(localeNl);

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'nl' } 
  ]
})
export class UsersModule { }
