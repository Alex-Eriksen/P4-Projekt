import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-buymenu3',
  templateUrl: './buymenu3.component.html',
  styleUrls: ['./buymenu3.component.css']
})
export class Buymenu3Component implements OnInit {

  // Test Dialog i Header ved play knappen. Ændrer open funkionen hvis du vil teste de forskellige dialogs

  constructor(public dialogRef: MatDialogRef<Buymenu3Component>, @Inject(MAT_DIALOG_DATA) public data: any ) { }

  ngOnInit(): void {
    // Tager fat i cdk-overlay-container i body og tilføjer en klasse så dens position absolute kan manipuleres
    document.body.children[6].classList.add('buy-menu-overlay3'); // Tilføjer sagt klasse til sidste element i body
  }

  onClose(): void {
    this.dialogRef.close(); // Lukker dialog
  }

}
