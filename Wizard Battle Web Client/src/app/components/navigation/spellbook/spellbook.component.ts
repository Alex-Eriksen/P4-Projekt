import { Component, OnInit } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SpellService } from 'src/app/services/spell.service';
import { SpellbookService } from 'src/app/services/spellbook.service';
import { StaticSpellResponse } from 'src/app/_models/Spell';
import { DirectSpellBookResponse, SpellBookRequest, StaticSpellBookResponse } from 'src/app/_models/SpellBook';
import { SpellSelectionComponent } from '../../modals/spell-selection/spell-selection.component';
import { PlayerService } from 'src/app/services/player.service';
import { PlayerRequest } from 'src/app/_models/Player';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-spellbook',
  templateUrl: './spellbook.component.html',
  styleUrls: ['./spellbook.component.css']
})
export class SpellbookComponent implements OnInit {

  	constructor(private spellService: SpellService, private spellBookService: SpellbookService, private authenticationService: AuthenticationService, private dialog: MatDialog, private playerService: PlayerService, private notiService: NotificationService) { }

	private playerId: number;
	private playerRequest: PlayerRequest = { accountID: null, playerName: "", iconID: 0, experiencePoints: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0, avgDamage: 0, avgSpellsHit: 0, spellBookID: 0};
	private spellBookRequest: SpellBookRequest;

	public spells: StaticSpellResponse[] = [];
	public currentSpells: StaticSpellResponse[] = [];
	public spellBooks: StaticSpellBookResponse[] = [];
	public spellLoadout: StaticSpellResponse[] = [];

	public openSpellBook: DirectSpellBookResponse;
	public equippedSpellBook : number;

	private placeholderSpell: StaticSpellResponse = { spellID: 0, spellName: `placeholder`, spellDescription: "", icon: {iconID: 18, iconName: "../../../../assets/spell-icons/choose-spell.png"}, spellTypeID: 0, schoolCategoryID: 0, damageAmount: 0, manaCost: 0, lifeTime:0, castTime: 0 };


  	ngOnInit(): void {
		while(this.playerId == undefined) {
			this.playerId = JwtDecodePlus.jwtDecode(this.authenticationService.AccessToken).nameid;
			if(this.playerId != undefined) {
				this.playerService.getById(this.playerId).subscribe({ // Gets player
					next: (res) => {
						this.equippedSpellBook = res.spellBookID;
						this.playerRequest = {
								accountID: null, playerName: res.playerName, iconID: res.icon.iconID, experiencePoints: res.experiencePoints,
								knowledgePoints: res.knowledgePoints, timeCapsules: res.timeCapsules, matchWins: res.matchWins,
								matchLosses: res.matchLosses, timePlayedMin: res.timePlayedMin, avgDamage: res.avgDamage,
								avgSpellsHit: res.avgSpellsHit, spellBookID: res.spellBookID
						};
						this.getBooks();
					},
					error: (err) => {
						console.error(Object.values(err.error.errors).join(', '));
						this.notiService.error(err.err.errors);
					}
				})
			}
		}
  	}


	private getBooks(): void {
		this.spellBookService.getAll().subscribe({ // Gets SpellBooks
			next: (res) => {
				if(this.playerId != undefined) {
					this.spellBooks = res.filter(spellbook => spellbook.playerID == this.playerId); // Filters personal spellbooks
					this.getSpellBook(this.equippedSpellBook, `Opened ${this.equippedSpellBook}`); // Gets current
				}
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
				this.notiService.error(err.err.errors);
			}
		});

		this.spellService.getAll().subscribe({ // Gets Spells
			next: (res) => {
				this.spells = res;
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
				this.notiService.error(err.err.errors);
			}
		});
	}

	openSpellSelection(spell: StaticSpellResponse): void {
		let spellIndex = this.openSpellBook.spells.map(function (spell) {return spell.spellName}).indexOf(spell.spellName); // Gets index
		let dialogRef = this.dialog.open(SpellSelectionComponent, {
			width: '685px',
			maxWidth: '100vw',
			height: '460px',
			exitAnimationDuration: "0s",
			hasBackdrop: true,
			backdropClass: "backdrop-bg-none",
			disableClose: true,
			data: this.openSpellBook
		  });

		  dialogRef.afterClosed().subscribe((spell: StaticSpellResponse) => {
			if(spell != null) {
				if(this.openSpellBook.spells.filter(x => x === spell)) {
					for (let i = 0; i < this.openSpellBook.spells.length; i++) {
						if(this.openSpellBook.spells[i].spellID === spell.spellID) {
							this.placeholderSpell.spellName = `placeholder ${i+1}`;
							this.openSpellBook.spells[i] = this.placeholderSpell;
							this.notiService.info(`Removed spell at slot ${i+1}, and replaced at ${spellIndex+1}`);
							break;
						}
					}
				}
				this.openSpellBook.spells[spellIndex] = spell; // Assign chosen spell to spells index
			}
		  });
	}

	getSpellBook(spellBookId: number, message: string): void {
		this.spellBookService.getById(spellBookId).subscribe({
			next: (res) => {
				this.openSpellBook = res;
				this.notiService.info(message);
				if(this.openSpellBook.spells.length != 8) {
					// let lockedSpell: StaticSpellResponse = { spellID: 0, spellName: "locked", spellDescription: "", icon: {iconID: 1337, iconName: "../../../../assets/spell-icons/locked-padlock.png"}, castTime: 0, damageAmount: 0, manaCost: 0 };
					for(let i = this.openSpellBook.spells.length; i < 8; i++) {
						this.placeholderSpell.spellName = `placeholder ${i+1}`;
						this.openSpellBook.spells.push(this.placeholderSpell);
					}
				}
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
				this.notiService.error(err.err.errors);
			}
		})
	}

	equipSpellBook(): void {
		console.log(this.openSpellBook.spellBookID);
		this.playerRequest.spellBookID = this.openSpellBook.spellBookID;
		this.playerService.update(this.playerId, this.playerRequest).subscribe({
			next: (res) => {
				this.equippedSpellBook = res.spellBookID;
				this.notiService.success(`Equipped ${this.playerRequest.spellBookID}`);
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
				this.notiService.error(err.err.errors);
			}
		})
	}

	saveSpellBook(): void {
		if(this.openSpellBook.spells.filter(spell => spell.spellID != 0).length != 8) {
			this.notiService.error("All spell slots must be assigned a spell!");
			return;
		}
		this.spellBookRequest = { spellBookName: this.openSpellBook.spellBookName, playerID: this.playerId, spellIDs: this.openSpellBook.spells.length == 0 ? [] : this.openSpellBook.spells.filter(spell => !spell.spellName.includes('placeholder')).map(spell => spell.spellID) }
		this.spellBookService.update(this.openSpellBook.spellBookID, this.spellBookRequest).subscribe({
			next: () => {
				this.notiService.success(`Saved ${this.spellBookRequest.spellBookName}`);
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
				this.notiService.error(err.err.errors);
			}
		})
	}
}
