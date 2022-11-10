import { Component, HostListener } from '@angular/core';
import { NavigationEnd, NavigationStart, Router, RoutesRecognized } from '@angular/router';
import { Subject } from 'rxjs';
import { JwtDecodePlus } from './helpers/JWTDecodePlus';
import { AuthenticationService } from './services/authentication.service';
import { PlayerService } from './services/player.service';
import { filter, pairwise } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'WizardBattle';
  showChat: boolean = true;
  showHeader: boolean = true;

  // Show header if client is not logging in or signing up
  constructor(private router: Router) {
    router.events.forEach((event) => {
      if(event instanceof NavigationEnd) {
        if(event['url'].includes("/login") || event['url'].includes('/signup')) {
          this.showHeader = false;
        } else {
          this.showHeader = true;
        }
      }
    })
  }
  toggleChat() {
    this.showChat = !this.showChat;
  }
}
