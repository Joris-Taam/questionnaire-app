import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from './form/form.component';
import { ThanksPageComponent } from './thanks-page/thanks-page.component';

const routes: Routes = [
  { path: 'form/:id', component: FormComponent },
  { path: 'form/:id/preview', component: FormComponent },
  { path: 'form', component: FormComponent }, 
  { path: 'thanks-page', component: ThanksPageComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
