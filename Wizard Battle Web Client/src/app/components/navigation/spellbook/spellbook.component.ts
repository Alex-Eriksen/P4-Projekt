import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SpellService } from 'src/app/services/spell.service';
import { SpellbookService } from 'src/app/services/spellbook.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';
import { DirectSpellBookResponse, SpellBookRequest, StaticSpellBookResponse } from 'src/app/_models/SpellBook';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

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
				this.spellBooks = data.filter(spellbook => spellbook.playerID == this.playerId); // Filters personal spellbooks
				this.getSpellBook(this.spellBooks[0].spellBookID); // Gets first spellbook as default
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		});
  	}

	drop(event: CdkDragDrop<StaticSpellResponse[]>) {
		if (event.previousContainer === event.container) {
			console.log(event.currentIndex);
			console.log(event.previousIndex);
		  	moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
		  	moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);

		} else {
			console.log(event.previousIndex);
			console.log(event.currentIndex);
		  transferArrayItem(
			event.previousContainer.data,
			event.container.data,
			event.previousIndex,
			event.currentIndex,
		  );
		}
	  }

	getSpellBook(spellBookId: number): void {
		this.spellBookService.getById(spellBookId).subscribe({
			next: (data) => {
				this.openSpellBook = data;
				this.currentSpells = data.spells;
				if(this.currentSpells.length != 8) {
					for (let i = this.currentSpells.length + 1; i < 9; i++) {
						this.currentSpells.push(this.spells[0])
						console.log(this.currentSpells.length);
					}
				}
				this.spellBookRequest.spellIDs = this.openSpellBook.spells.map(spell => spell.spellID);
				console.log("Fetched spellbook: " + data.spellBookID);
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			}
		})
	}
}
