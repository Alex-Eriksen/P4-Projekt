import { DirectSchoolCategoryResponse } from './../../../_models/SchoolCategory/DirectSchoolCategoryResponse';
import { SchoolCategoryService } from './../../../services/school-category.service';
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

	constructor(private spellService: SpellService, public dialogRef: MatDialogRef<SpellSelectionComponent>, @Inject(MAT_DIALOG_DATA) public data: DirectSpellBookResponse, private schoolCategoryService: SchoolCategoryService) { }

	public nextPageSubject: Subject<number> = new Subject<number>();
	public previousPageSubject: Subject<number> = new Subject<number>();

	public spells: StaticSpellResponse[] = [];

	public currentIndex: number = 0;

	public pageClassString: string = "";
	public leftSideClassString: string = "active-mid-to-left";
	public rightSideClassString: string = "";

	public categories: DirectSchoolCategoryResponse[] = [];

	ngOnInit(): void {
		document.body.children[6].classList.add('spell-selection-overlay');
		for(let i = 0; i < 100; i++) {
			if(document.getElementById(`cdk-overlay-${i}`) != undefined) {
				document.getElementById(`cdk-overlay-${i}`)?.classList.add("spell-selection-position");
			}
		}

		for (let i = 1; i < 10; i++) {
			this.schoolCategoryService.getById(i).subscribe(x => {
				this.categories.push(x);
			});
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

	@HostListener('document:keydown', ['$event'])
	handleKeyboardEvent(event: KeyboardEvent) {
		if(!isFinite(parseInt(event.key))) {
			return;
		}

		if(event.key == "0") {
			this.resetPage();
		}
		else if(event.key == "1" && this.currentIndex == 0) {
			this.nextPage();
		}
		else {
			this.goToPage(parseInt(event.key));
		}

	}

	nextPage(): void {
		if(this.currentIndex == this.spells.length)
			return

		this.currentIndex += 1;
		this.nextPageSubject.next(this.currentIndex);
		if(this.currentIndex == 1) {
			this.rightSideClassString = "inactive-right-to-mid";
			this.leftSideClassString = "inactive-left-side";
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
			this.leftSideClassString = "";
			this.pageClassString = "active-page"
		}
	}

	resetPage(): void {
		if(this.currentIndex != 0) {
			this.currentIndex = 0;
			this.previousPageSubject.next(this.currentIndex);

			this.rightSideClassString = "active-mid-to-right"
			this.leftSideClassString = "";
			this.pageClassString = "active-page"
		}
	}

	goToPage(pageNum: number): void {
		if(pageNum == this.currentIndex) {
			return;
		}

		// if started at 0
		if(this.currentIndex == 0) {
			if(pageNum == 1) {
				this.nextPage();
			} else {
				this.nextPageSubject.next(pageNum);
				this.rightSideClassString = "inactive-right-to-mid";
				this.leftSideClassString = "inactive-left-side";
				this.pageClassString = "inactive-page"
			}
		}

		// if pageNum is bigger than currentIndex use nextPage
		if(pageNum > this.currentIndex) {
			this.nextPageSubject.next(pageNum);
		} else { // if PageNum is lower than currentIndex use previousPage
			this.previousPageSubject.next(pageNum);
		}

		this.currentIndex = pageNum;
	}

	equipSpell = () => { this.onClose(this.spells[this.currentIndex-1]); }

	onClose = (spell?: StaticSpellResponse) => { this.dialogRef.close(spell); }
}
