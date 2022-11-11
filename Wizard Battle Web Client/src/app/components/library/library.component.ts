import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Buymenu1Component } from '../modals/buy-menues/buymenu1/buymenu1.component';
import { Buymenu2Component } from '../modals/buy-menues/buymenu2/buymenu2.component';
import { Buymenu3Component } from '../modals/buy-menues/buymenu3/buymenu3.component';
import { Buymenu4Component } from '../modals/buy-menues/buymenu4/buymenu4.component';
import { Buymenu5Component } from '../modals/buy-menues/buymenu5/buymenu5.component';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openBuyMenu1(): void {
    let element = document.getElementById("chat-tab");
    element!.scrollTop = 0;
    let dialogRef = this.dialog.open(Buymenu1Component, {
      width: '500px',
      maxWidth: '50vw',
      height: '402.34px',
      backdropClass: 'cdk-overlay-transparent-backdrop',
      disableClose: true,
      enterAnimationDuration: "0s",
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
      document.body.children[6].classList.remove('buy-menu-overlay1');
    });
  }

  openBuyMenu2(): void {
    let element = document.getElementById("chat-tab");
    element!.scrollTop = 0;
    let dialogRef = this.dialog.open(Buymenu2Component, {
      width: '488px',
      maxWidth: '50vw',
      height: '402.34px',
      backdropClass: 'cdk-overlay-transparent-backdrop',
      disableClose: true,
      enterAnimationDuration: "0s",

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
      document.body.children[6].classList.remove('buy-menu-overlay2');
    });
  }

  openBuyMenu3(): void {
    let element = document.getElementById("chat-tab");
    element!.scrollTop = 0;
    let dialogRef = this.dialog.open(Buymenu3Component, {
      width: '488px',
      maxWidth: '50vw',
      height: '402.34px',
      backdropClass: 'cdk-overlay-transparent-backdrop',
      disableClose: true,
      enterAnimationDuration: "0s",

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
      document.body.children[6].classList.remove('buy-menu-overlay3');
    });
  }

  openBuyMenu4(): void {
    let element = document.getElementById("chat-tab");
    element!.scrollTop = element!.scrollHeight;
    let dialogRef = this.dialog.open(Buymenu4Component, {
      width: '488px',
      maxWidth: '50vw',
      height: '402.34px',
      backdropClass: 'cdk-overlay-transparent-backdrop',
      disableClose: true,
      enterAnimationDuration: "0s",

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
      document.body.children[6].classList.remove('buy-menu-overlay4');
    });
  }

  openBuyMenu5(): void {
    let element = document.getElementById("chat-tab");
    element!.scrollTop = element!.scrollHeight;
    let dialogRef = this.dialog.open(Buymenu5Component, {
      width: '488px',
      maxWidth: '50vw',
      height: '402.34px',
      backdropClass: 'cdk-overlay-transparent-backdrop',
      disableClose: true,
      enterAnimationDuration: "0s",

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
      document.body.children[6].classList.remove('buy-menu-overlay5');
    });
  }
}
