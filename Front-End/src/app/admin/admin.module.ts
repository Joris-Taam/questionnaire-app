import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { RouterLink } from '@angular/router';
import { QuestionnaireOverviewComponent } from './questionnaire-overview/questionnaire-overview.component';

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    RouterLink,
    AdminRoutingModule

  ]
})
export class AdminModule { }
