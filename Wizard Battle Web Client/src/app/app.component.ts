import { Component } from '@angular/core';
import { NavigationEnd, NavigationStart, Router } from '@angular/router';

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
    console.log(this.showChat);
  }
}
