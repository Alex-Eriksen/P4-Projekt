import { Component, Input, OnInit } from '@angular/core';
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

	@Input() public nextPage: Observable<number>;
	@Input() public previousPage: Observable<number>;


	public pageClassString: string = "";
	public leftSideClassString: string = "";
	public rightSideClassString: string = "";

  	ngOnInit(): void {
		this.nextPage.subscribe((currentIndex) => this.onNextPage(currentIndex));
		this.previousPage.subscribe((currentIndex) => this.onPrevPage(currentIndex));
		this.onNextPage(0);
	}

	onNextPage(currentIndex: number): void {
		if(this.spell.spellID == currentIndex) { // Page that gets active.
			// Left side start in the middle, and turns 90deg to the left.
			this.leftSideClassString = "active-mid-to-left";
			console.log(currentIndex);

			// Right side does not move.
			this.rightSideClassString = "";

			// Page gets active
			this.pageClassString = "active-page"
		}
		else if((this.spell.spellID + 1) == currentIndex) { // Page that gets inactive.
			// Right side starts from the right side and turns 90deg to the left.
			this.rightSideClassString = "inactive-right-to-mid";

			// Left side does not move
			this.leftSideClassString = "inactive-left-side";

			this.pageClassString = "inactive-page"
		}
		else { // Page that gets inactive
			this.pageClassString = "inactive"
		}
	}

	onPrevPage(currentIndex: number): void {
		if(this.spell.spellID == currentIndex){
			// Right side starts in the middle, and turns 90deg to the right.
			this.rightSideClassString = "active-mid-to-right"

			// Left side does not move.
			this.leftSideClassString = "";

			// Page gets active
			this.pageClassString = "active-page"

		} else if((this.spell.spellID - 1) == currentIndex) { // Page that gets inactive.
			// Left side turns 90deg towards to the middle.
			this.leftSideClassString = "inactive-left-to-mid";

			// Right side does not move
			this.rightSideClassString = "inactive-right-side";

			// Page gets inactive
			this.pageClassString = "inactive-page"

		} else { // Page that gets inactive
			this.pageClassString = "inactive"
		}
	}
}
