import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-change-icon',
  templateUrl: './change-icon.component.html',
  styleUrls: ['./change-icon.component.css']
})
export class ChangeIconComponent implements OnInit {

  images: string[] = [];

  constructor(public dialogRef: MatDialogRef<ChangeIconComponent>, @Inject(MAT_DIALOG_DATA) public data: any ) { }

  ngOnInit(): void {
    // Tager fat i cdk-overlay-container i body og tilføjer en klasse så dens position absolute kan manipuleres
    document.body.children[6].classList.add('change-icon-overlay'); // Tilføjer sagt klasse til sidste element i body

    for(let i = 0; i < 35; i++) {
      this.images.push("../../../../assets/Wiz Profil Pic 1.png");
    }
  }

  onClose(): void {
    this.dialogRef.close(); // Lukker dialog
  }
}
