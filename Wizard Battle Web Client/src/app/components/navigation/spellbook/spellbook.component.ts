import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SpellService } from 'src/app/services/spell.service';
import { SpellbookService } from 'src/app/services/spellbook.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';
import { DirectSpellBookResponse, SpellBookRequest, StaticSpellBookResponse } from 'src/app/_models/SpellBook';
import { SpellSelectionComponent } from '../../modals/spell-selection/spell-selection.component';
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-spellbook',
  templateUrl: './spellbook.component.html',
  styleUrls: ['./spellbook.component.css']
})
export class SpellbookComponent implements OnInit {

  	constructor(private spellService: SpellService, private spellBookService: SpellbookService, private authenticationService: AuthenticationService, private dialog: MatDialog) { }

	private playerId: number;

	public spells: StaticSpellResponse[] = [];

	public currentSpells: StaticSpellResponse[] = [];

	public spellBooks: StaticSpellBookResponse[] = [];

	public openSpellBook: DirectSpellBookResponse;

	public spellBookRequest: SpellBookRequest = { spellBookName: "", playerID: 0, spellIDs: [] }

	public spellLoadout: StaticSpellResponse[] = [];

	public hasChanged: boolean = false;

  	ngOnInit(): void {
		this.playerId = JwtDecodePlus.jwtDecode(this.authenticationService.AccessToken).nameid; // Gets ID
		this.spellService.getAll().subscribe({ // Gets Spells
			next: (data) => {
				this.spells = data;
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		});

		this.spellBookService.getAll().subscribe({ // Gets SpellBooks
			next: (data) => {
				if(this.playerId != null) {
					this.spellBooks = data.filter(spellbook => spellbook.playerID == this.playerId); // Filters personal spellbooks
					this.getSpellBook(this.spellBooks[0].spellBookID); // Gets first spellbook as default
				}
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		});
  	}

	openSpellSelection(spellId: number): void {
		let dialogRef = this.dialog.open(SpellSelectionComponent, {
			width: '512px',
			maxWidth: '100vw',
			height: '524px',
			exitAnimationDuration: "0s",
			hasBackdrop: true,
			backdropClass: "backdrop-bg-none",
			disableClose: false,
			data: this.openSpellBook
		  });

		  dialogRef.afterClosed().subscribe((id) => {
			if(id != null) {
				for(let i = 0; i < 8; i++) {
					if(this.openSpellBook.spells[i].spellID == spellId) {
						this.openSpellBook.spells[i] = this.spells.find(x => x.spellID == id)!;
					}
				}
			}
		  });
	}

	getSpellBook(spellBookId: number): void {
		this.spellBookService.getById(spellBookId).subscribe({
			next: (data) => {
				this.openSpellBook = data;
				if(this.openSpellBook.spells.length != 8) {
					let placeholderSpell: StaticSpellResponse = { spellID: 0, spellName: "placeholder", spellDescription: "", icon: {iconID: 18, iconName: "../../../../assets/spell-icons/choose-spell.png"}, castTime: 0, damageAmount: 0, manaCost: 0 };
					let lockedSpell: StaticSpellResponse = { spellID: 0, spellName: "locked", spellDescription: "", icon: {iconID: 1337, iconName: "../../../../assets/spell-icons/locked-padlock.png"}, castTime: 0, damageAmount: 0, manaCost: 0 };
					for(let i = this.openSpellBook.spells.length; i < 8; i++) {
						this.openSpellBook.spells.push(placeholderSpell);
					}
				}
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		})
	}

	saveSpellBook(): void {

		this.spellBookRequest = { spellBookName: this.openSpellBook.spellBookName, playerID: this.playerId, spellIDs: this.openSpellBook.spells.filter(spell => spell.spellID != 0).map(spell => spell.spellID) }
		console.log(this.spellBookRequest);
		this.spellBookService.update(this.openSpellBook.spellBookID, this.spellBookRequest).subscribe({
			next: (data) => {
				console.log(data);
				localStorage.setItem("spell-order", JSON.stringify(this.spellBookRequest.spellIDs));
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		})
	}
}
