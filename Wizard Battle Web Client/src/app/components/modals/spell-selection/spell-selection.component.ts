import { AfterContentInit, AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ContentChildren, ElementRef, HostListener, Inject, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { SpellService } from 'src/app/services/spell.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';
import { DirectSpellBookResponse } from 'src/app/_models/SpellBook';

@Component({
  selector: 'app-spell-selection',
  templateUrl: './spell-selection.component.html',
  styleUrls: ['./spell-selection.component.css']
})
export class SpellSelectionComponent implements OnInit {

	constructor(private spellService: SpellService, public dialogRef: MatDialogRef<SpellSelectionComponent>, @Inject(MAT_DIALOG_DATA) public data: DirectSpellBookResponse) { }

	public nextPageSubject: Subject<number> = new Subject<number>();
	public previousPageSubject: Subject<number> = new Subject<number>();

	public spells: StaticSpellResponse[] = [];

	public currentIndex: number = 0;

	public pageClassString: string = "";
	public leftSideClassString: string = "active-mid-to-left";
	public rightSideClassString: string = "";


	ngOnInit(): void {
		document.body.children[6].classList.add('spell-selection-overlay');
		for(let i = 0; i < 100; i++) {
			if(document.getElementById(`cdk-overlay-${i}`) != undefined) {
				document.getElementById(`cdk-overlay-${i}`)?.classList.add("spell-selection-position");
			}
		}

		this.spellService.getAll().subscribe({ // Gets Spells
			next: (data) => {
				this.spells = data;
				for(let i = 0; i < this.data.spells.length; i++) {
					this.spells = this.spells.filter(x => x.spellID !== this.data.spells[i].spellID);
				}
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		});
	}

	nextPage(): void {
		if(this.currentIndex == this.spells.length)
			return


		this.currentIndex += 1;
		this.nextPageSubject.next(this.currentIndex);
		if(this.currentIndex == 1) {
			// Right side starts from the right side and turns 90deg to the left.
			this.rightSideClassString = "inactive-right-to-mid";

			// Left side does not move
			this.leftSideClassString = "inactive-left-side";

			// Page gets active
			this.pageClassString = "inactive-page"
		}
	}

	previousPage(): void {
		if(this.currentIndex == 0){
			return;
		}
		this.currentIndex -= 1;
		this.previousPageSubject.next(this.currentIndex);
		if(this.currentIndex == 0) {
			this.rightSideClassString = "active-mid-to-right"

			// Left side does not move.
			this.leftSideClassString = "";

			// Page gets active
			this.pageClassString = "active-page"
		}
	}

	onClose = (spell: StaticSpellResponse) => { this.dialogRef.close(spell); }
}
