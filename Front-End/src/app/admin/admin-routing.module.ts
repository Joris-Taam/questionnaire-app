import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuestionnaireOverviewComponent } from './questionnaire-overview/questionnaire-overview.component';
import { ResultOverviewComponent } from './result-overview/result-overview.component';
import { BindUsersComponent } from './bind-users/bind-users.component';
import { QuestionOverviewComponent } from './question-overview/question-overview.component';
import { QuestionnaireEditComponent } from './questionnaire-edit/questionnaire-edit.component';
import { QuestionnaireAddComponent } from './questionnaire-add/questionnaire-add.component';
import { ResultDetailComponent } from './result-detail/result-detail.component';
import { ParticipantDetailComponent } from './participant-detail/participant-detail.component';
import { AdministrationListComponent } from './administration-list/administration-list.component';
import { AdministrationManagementComponent } from './administration-management/administration-management.component';
import { RelationManagementComponent } from './relation-management/relation-management.component';


const routes: Routes = [
  {path: '', component: QuestionnaireOverviewComponent},
  {path: 'questionnaireoverview', component: QuestionnaireOverviewComponent},
  {path: 'resultoverview', component: ResultOverviewComponent},
  {path: 'resultdetail', component: ResultDetailComponent},
  {path: 'bindusers', component: BindUsersComponent},
  {path: 'questionoverview', component: QuestionOverviewComponent},
  {path: 'questionnaireedit', component: QuestionnaireEditComponent },
  {path: 'questionnaireadd', component: QuestionnaireAddComponent},
  {path: 'questionnaireadd', component: QuestionnaireAddComponent},
  { path: 'participantdetail', component: ParticipantDetailComponent },
  { path: 'administrationlist', component: AdministrationListComponent },
  { path: 'administrationmanagement', component: AdministrationManagementComponent },
  { path: 'relationmanagement', component: RelationManagementComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
