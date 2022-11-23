import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, HostListener, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SpellService } from 'src/app/services/spell.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';
import { DirectSpellBookResponse } from 'src/app/_models/SpellBook';

@Component({
  selector: 'app-spell-selection',
  templateUrl: './spell-selection.component.html',
  styleUrls: ['./spell-selection.component.css']
})
export class SpellSelectionComponent implements OnInit, AfterViewInit {

	constructor(private spellService: SpellService, public dialogRef: MatDialogRef<SpellSelectionComponent>, @Inject(MAT_DIALOG_DATA) public data: DirectSpellBookResponse, private cdr: ChangeDetectorRef, private elementRef: ElementRef) { }

	public spells: StaticSpellResponse[] = [];

	public scroll: any;

	anchorHref: number = 0;

	reachedBottom: boolean = false;
	reachedTop: boolean = true;

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

	ngAfterViewInit(): void {
		this.scroll = this.elementRef.nativeElement.querySelector('#scroll');
		this.scroll.addEventListener('scroll', () => {
			if(this.scroll.scrollTop < 80) {
				this.reachedTop = true;
			} else {
				this.reachedTop = false;
			}

			if( (this.scroll.scrollTop + 374) >= (this.scroll.scrollHeight - 80) ) {
				this.reachedBottom = true;
			} else {
				this.reachedBottom = false;
			}
		});
	}

	goUp = () => { this.scroll.scrollTop -= 374; }

	goDown = () => { this.scroll.scrollTop += 374; }

	onClose = (spellId?: number) => { this.dialogRef.close(spellId); }
}
