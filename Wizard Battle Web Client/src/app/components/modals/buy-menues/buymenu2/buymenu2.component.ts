import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-buymenu2',
  templateUrl: './buymenu2.component.html',
  styleUrls: ['./buymenu2.component.css']
})
export class Buymenu2Component implements OnInit {

  // Test Dialog i Header ved play knappen. Ændrer open funkionen hvis du vil teste de forskellige dialogs

  constructor(public dialogRef: MatDialogRef<Buymenu2Component>, @Inject(MAT_DIALOG_DATA) public data: any ) { }

  ngOnInit(): void {
    // Tager fat i cdk-overlay-container i body og tilføjer en klasse så dens position absolute kan manipuleres
    document.body.children[6].classList.add('buy-menu-overlay2'); // Tilføjer sagt klasse til sidste element i body
  }

  onClose(): void {
    this.dialogRef.close(); // Lukker dialog
  }

}