import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SpellService } from 'src/app/services/spell.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';

@Component({
  selector: 'app-spell-selection',
  templateUrl: './spell-selection.component.html',
  styleUrls: ['./spell-selection.component.css']
})
export class SpellSelectionComponent implements OnInit {

	constructor(private spellService: SpellService, public dialogRef: MatDialogRef<SpellSelectionComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

	public spells: StaticSpellResponse[] = [];

	ngOnInit(): void {
		for(let i = 0; i < 100; i++) {
			if(document.getElementById(`mat-dialog-${i}`) != undefined)
				document.getElementById(`mat-dialog-${i}`)!.classList.add('spell-selection-overlay');
		}
		this.spellService.getAll().subscribe({ // Gets Spells
			next: (data) => {
				this.spells = data;
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		});
	}

	onClose(spellId?: number): void {
		this.dialogRef.close(spellId);
	}
}
