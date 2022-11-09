import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-buymenu1',
  templateUrl: './buymenu1.component.html',
  styleUrls: ['./buymenu1.component.css']
})

// Navn kan ændres
export class Buymenu1Component implements OnInit {

  // Test Dialog i Header ved play knappen. Ændrer open funkionen hvis du vil teste de forskellige dialogs

  constructor(public dialogRef: MatDialogRef<Buymenu1Component>, @Inject(MAT_DIALOG_DATA) public data: any ) { }

  ngOnInit(): void {
    // Tager fat i cdk-overlay-container i body og tilføjer en klasse så dens position absolute kan manipuleres
    document.body.children[6].classList.add('buy-menu-overlay1'); // Tilføjer sagt klasse til sidste element i body
  }

  onClose(): void {
    this.dialogRef.close(); // Lukker dialog
  }

}
