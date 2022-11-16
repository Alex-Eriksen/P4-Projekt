import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/navigation/home/home.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { ProfileComponent } from './components/navigation/profile/profile.component';
import { SignupComponent } from './components/authentication/signup/signup.component';
import { SpellbookComponent } from './components/spellbook/spellbook.component';
import { LibraryComponent } from './components/navigation/library/library.component';
import { AuthenticationGuard } from './services/authentication.guard.service';
import { ChangeIconComponent } from './components/modals/change-icon/change-icon.component';

const routes: Routes = [
  { path: '',  component: HomeComponent, canActivate: [AuthenticationGuard]},
  { path: 'library',  component: LibraryComponent, canActivate: [AuthenticationGuard]},
  { path: 'spellbook',  component: SpellbookComponent, canActivate: [AuthenticationGuard]},
  { path: 'login',  component: LoginComponent },
  { path: 'signup',  component: SignupComponent },
 	{path: 'profile', component: ProfileComponent, canActivate: [AuthenticationGuard]},
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
