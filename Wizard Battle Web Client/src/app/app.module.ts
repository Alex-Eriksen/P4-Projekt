import { APP_INITIALIZER, DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import localeDk from '@angular/common/locales/en-DK';
registerLocaleData(localeDk);

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/fixed-components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SpellbookComponent } from './components/spellbook/spellbook.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { AuthenticationInterceptor } from './_interceptor/authentication.intercepter';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './services/authentication.service';
import { ChatComponent } from './components/fixed-components/chat/chat.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ChatBoxComponent } from './components/fixed-components/chat-box/chat-box.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LibraryComponent } from './components/library/library.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    SignupComponent,
    SpellbookComponent,
    ChatComponent,
    ChatBoxComponent
    ProfileComponent
    LibraryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ScrollingModule
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
