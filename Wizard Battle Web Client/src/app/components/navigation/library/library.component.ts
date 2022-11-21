import { Component, OnInit } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {

  showBuyMenu1: boolean = false;
  showBuyMenu2: boolean = false;
  showBuyMenu3: boolean = false;
  showBuyMenu4: boolean = false;
  showBuyMenu5: boolean = false;

  constructor(private chatService: ChatService) { }

	value: number = 0;
  isOpen: boolean = true;

  ngOnInit(): void {

    this.isOpen = true;

    this.chatService.OnChatChanged.subscribe((x) => {
        this.isOpen = x;
    });
  }
}
