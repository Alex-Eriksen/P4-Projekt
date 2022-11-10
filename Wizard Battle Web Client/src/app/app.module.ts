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
import { ChatBoxComponent } from './components/fixed-components/chat-box/chat-box.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LibraryComponent } from './components/library/library.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from  '@angular/material/dialog';
import { ChangeIconComponent } from './components/modals/change-icon/change-icon.component';
import { Buymenu1Component } from './components/modals/buy-menues/buymenu1/buymenu1.component';
import { Buymenu2Component } from './components/modals/buy-menues/buymenu2/buymenu2.component';
import { Buymenu3Component } from './components/modals/buy-menues/buymenu3/buymenu3.component';
import { Buymenu4Component } from './components/modals/buy-menues/buymenu4/buymenu4.component';
import { Buymenu5Component } from './components/modals/buy-menues/buymenu5/buymenu5.component';
import { SkinInfoComponent } from './components/modals/skin-info/skin-info.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    SignupComponent,
    SpellbookComponent,
    ChatComponent,
    ChatBoxComponent,
    ProfileComponent,
    LibraryComponent,
    ChangeIconComponent,
    Buymenu1Component,
    Buymenu2Component,
    Buymenu3Component,
    Buymenu4Component,
    Buymenu5Component,
    SkinInfoComponent
  ],
	imports: [
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		BrowserAnimationsModule,
		FormsModule,
		MatDialogModule
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
