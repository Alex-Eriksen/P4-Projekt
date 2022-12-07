import { DirectSchoolCategoryResponse } from './../../../../_models/SchoolCategory/DirectSchoolCategoryResponse';
import { Component, Input, OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs';
import { StaticSpellResponse } from 'src/app/_models/Spell';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.css']
})
export class PageComponent implements OnInit {

  	constructor() { }

  	@Input() public spell: StaticSpellResponse;
	@Input() public schoolCategories: DirectSchoolCategoryResponse[];
	@Input() public nextPage: Observable<number>;
	@Input() public previousPage: Observable<number>;


	public pageClassString: string = "";
	public leftSideClassString: string = "";
	public rightSideClassString: string = "";
	public wasActive = false;
	public schoolCategory: DirectSchoolCategoryResponse;

  	ngOnInit(): void {
		this.nextPage.subscribe((currentIndex) => this.onNextPage(currentIndex));
		this.previousPage.subscribe((currentIndex) => this.onPrevPage(currentIndex));
		this.onNextPage(0);
		this.schoolCategory = this.schoolCategories.find(x => x.schoolCategoryID == this.spell.schoolCategoryID)!;
	}

	onNextPage(currentIndex: number): void {
		if(this.spell.spellID == currentIndex) {
			// Left side start in the middle, and turns 90deg to the left.
			this.leftSideClassString = "active-mid-to-left";

			// Right side does not move.
			this.rightSideClassString = "";

			// Page gets active
			this.pageClassString = "active-page"
			this.wasActive = true;
		}
		else if(this.wasActive) {
			// Right side starts from the right side and turns 90deg to the left.
			this.rightSideClassString = "inactive-right-to-mid";

			// Left side does not move
			this.leftSideClassString = "inactive-left-side";

			// Page gets inactive
			this.pageClassString = "inactive-page"
			this.wasActive = false;
		}
		else {
			this.pageClassString = "inactive"
			this.leftSideClassString = "";
			this.rightSideClassString = "";
		}
	}

	onPrevPage(currentIndex: number): void {
		if(this.spell.spellID == currentIndex) {
			// Right side starts in the middle, and turns 90deg to the right.
			this.rightSideClassString = "active-mid-to-right";

			// Left side does not move.
			this.leftSideClassString = "";

			// Page gets active
			this.pageClassString = "active-page"
			this.wasActive = true;
		}
		else if(this.wasActive) {
			// Left side turns 90deg towards to the middle.
			this.leftSideClassString = "inactive-left-to-mid";

			// Right side does not move
			this.rightSideClassString = "inactive-right-side";

			// Page gets inactive
			this.pageClassString = "inactive-page"
			this.wasActive = false;
		}
		else {
			this.pageClassString = "inactive"
			this.leftSideClassString = "";
			this.rightSideClassString = "";
		}
	}
}
