import { Component, HostListener, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Router, RoutesRecognized } from '@angular/router';
import { Subject } from 'rxjs';
import { JwtDecodePlus } from './helpers/JWTDecodePlus';
import { AuthenticationService } from './services/authentication.service';
import { PlayerService } from './services/player.service';
import { filter, pairwise } from 'rxjs/operators';
import { ChatService } from './services/chat.service';

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
  constructor(private router: Router, private chatService: ChatService) {
    router.events.forEach((event) => {
      if(event instanceof NavigationEnd) {
        if(event['url'].includes("/login") || event['url'].includes('/signup')) {
          this.showHeader = false;
		  document.body.style.backgroundImage = "url('./assets/texture.jpg')";
        } else {
          this.showHeader = true;
		  document.body.style.backgroundImage = "url('./assets/wizard-battle-border.png')";
        }
      }
    })
  }

  toggleChat() {
    this.showChat = !this.showChat;
	this.chatService.toggleChat(this.showChat);
  }
}
