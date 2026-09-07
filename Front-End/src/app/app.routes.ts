import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '@auth0/auth0-angular';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full', },
    { path: 'home', component: HomeComponent,  canActivate: [AuthGuard] },
    { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule), },
    { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),  canActivate: [AuthGuard]},

];
