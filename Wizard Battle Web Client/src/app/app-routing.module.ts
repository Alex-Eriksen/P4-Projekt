import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { ProfileComponent } from './components/profile/profile.component';
import { SignupComponent } from './components/signup/signup.component';
import { SpellbookComponent } from './components/spellbook/spellbook.component';
import { LibraryComponent } from './components/library/library.component';
import { AuthenticationGuard } from './services/authentication.guard.service';

const routes: Routes = [
  { path: '',  component: HomeComponent, canActivate: [AuthenticationGuard]},
  { path: 'library',  component: LibraryComponent, canActivate: [AuthenticationGuard]},
  { path: 'spellbook',  component: SpellbookComponent, canActivate: [AuthenticationGuard]},
  { path: 'login',  component: LoginComponent },
  { path: 'signup',  component: SignupComponent },
  {path: 'profile', component: ProfileComponent},
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
