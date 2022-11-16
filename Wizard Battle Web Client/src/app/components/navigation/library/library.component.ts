import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit {

  hideBuyMenu = true;

  constructor(private dialog: MatDialog) { }

	value: number = 0;

  ngOnInit(): void {

  }
}
