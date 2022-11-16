import { APP_INITIALIZER, DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import localeDk from '@angular/common/locales/en-DK';
registerLocaleData(localeDk);

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/navigation/home/home.component';
import { HeaderComponent } from './components/fixed-components/header/header.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { SignupComponent } from './components/authentication/signup/signup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SpellbookComponent } from './components/navigation/spellbook/spellbook.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { AuthenticationInterceptor } from './_interceptor/authentication.intercepter';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './services/authentication.service';
import { ChatBoxComponent } from './components/fixed-components/chat-box/chat-box.component';
import { ProfileComponent } from './components/navigation/profile/profile.component';
import { LibraryComponent } from './components/navigation/library/library.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from  '@angular/material/dialog';
import { ChangeIconComponent } from './components/modals/change-icon/change-icon.component';
import { SkinInfoComponent } from './components/modals/skin-info/skin-info.component';
import { ChatComponent } from './components/fixed-components/chat/chat.component';
import { AddFriendComponent } from './components/modals/add-friend/add-friend.component';
import { LeaderboardComponent } from './components/navigation/leaderboard/leaderboard.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    SignupComponent,
    SpellbookComponent,
    ChatBoxComponent,
    ProfileComponent,
    LibraryComponent,
    ChangeIconComponent,
    SkinInfoComponent,
    ChatComponent,
    AddFriendComponent,
    LeaderboardComponent
  ],
	imports: [
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		BrowserAnimationsModule,
		FormsModule,
		MatDialogModule,
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [ AuthenticationService ] },
		{ provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
		{ provide: LocationStrategy, useClass: HashLocationStrategy },
		{ provide: DEFAULT_CURRENCY_CODE, useValue: 'DKK' },
		{ provide: LOCALE_ID, useValue: 'en-DK' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
