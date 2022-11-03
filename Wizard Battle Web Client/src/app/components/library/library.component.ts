import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Buymenu1Component } from '../modals/change-icon/buy-menues/buymenu1/buymenu1.component';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openBuyMenu(): void {
    let dialogRef = this.dialog.open(Buymenu1Component, {
      width: '536px',
      maxWidth: '50vw',
      height: '449px',

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
    });
  }
}
