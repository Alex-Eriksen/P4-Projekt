import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {

  showBuyMenu1: boolean;
  showBuyMenu2: boolean;
  showBuyMenu3: boolean;
  showBuyMenu4: boolean;
  showBuyMenu5: boolean;

  constructor(private dialog: MatDialog, private chatService: ChatService) { 
    this.showBuyMenu1 = false;
    this.showBuyMenu2 = false;
    this.showBuyMenu3 = false;
    this.showBuyMenu4 = false;
    this.showBuyMenu5 = false;

  }

	value: number = 0;
  isOpen: boolean = true;

  ngOnInit(): void {

    this.chatService.OnChatChanged.subscribe((x) => {
      this.isOpen = x;
    });
  }
}
