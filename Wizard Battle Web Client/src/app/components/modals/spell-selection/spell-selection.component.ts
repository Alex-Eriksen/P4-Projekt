import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, HostListener, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SpellService } from 'src/app/services/spell.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';

@Component({
  selector: 'app-spell-selection',
  templateUrl: './spell-selection.component.html',
  styleUrls: ['./spell-selection.component.css']
})
export class SpellSelectionComponent implements OnInit, AfterViewInit {

	constructor(private spellService: SpellService, public dialogRef: MatDialogRef<SpellSelectionComponent>, @Inject(MAT_DIALOG_DATA) public data: any, private cdr: ChangeDetectorRef, private elementRef: ElementRef) { }

	public spells: StaticSpellResponse[] = [];

	public scroll: any;

	anchorHref: number = 0;

	reachedBottom: boolean = false;
	reachedTop: boolean = true;

	ngOnInit(): void {
		document.body.children[6].classList.add('spell-selection-overlay');
		this.spellService.getAll().subscribe({ // Gets Spells
			next: (data) => {
				this.spells = data;
				this.spells = this.spells.concat(data);
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
