import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { SpellbookComponent } from './components/spellbook/spellbook.component';

const routes: Routes = [
  { path: '',  component: HomeComponent },
  { path: 'spellbook',  component: SpellbookComponent },
  { path: 'login',  component: LoginComponent },
  { path: 'signup',  component: SignupComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
