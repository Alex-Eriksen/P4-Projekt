import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { SkinService } from 'src/app/services/skin.service';
import { TransactionService } from 'src/app/services/transaction.service';
import { PlayerSkin } from 'src/app/_models/Misc/PlayerSkin';
import { PlayerRequest } from 'src/app/_models/Player';
import { StaticSkinItemResponse } from 'src/app/_models/SkinItem';
import { TransactionRequest } from 'src/app/_models/Transaction';

@Component({
  selector: 'app-skin-info',
  templateUrl: './skin-info.component.html',
  styleUrls: ['./skin-info.component.css']
})

export class SkinInfoComponent implements OnInit {

	public error: string = ""

	public hasPurchased: boolean = false;

	public isOwned: boolean = false;

	constructor(public dialogRef: MatDialogRef<SkinInfoComponent>, @Inject(MAT_DIALOG_DATA) public data: PlayerSkin, private playerService: PlayerService, private transactionService: TransactionService) { }

  	ngOnInit(): void {
		document.body.children[6].classList.add('skin-item-overlay');
		this.transactionService.getAll().subscribe((transactions) => {
			if(transactions.filter(transaction => transaction.playerID == this.data.player.playerID && transaction.skinID == this.data.skinItem.skinID).length != 0) {
				this.isOwned = true;
			}
		})
  	}

	unlockSkin(): void {
		if(this.data.player.timeCapsules >= this.data.skinItem.skinPrice) {
			let request: TransactionRequest = { skinID: this.data.skinItem.skinID, playerID: this.data.player.playerID, totalCost: this.data.skinItem.skinPrice }
			this.transactionService.create(request).subscribe({
				error: (err) => {
					console.error(Object.values(err.error.errors).join(', '));
				},
				complete: () => {
					this.data.player.timeCapsules = this.data.player.timeCapsules - request.totalCost;
					let playerRequest: PlayerRequest = Object.assign({},this.data.player, this.data.player.icon)
					this.playerService.update(this.data.player.playerID, playerRequest).subscribe({
						error: (err) => {
							console.error(Object.values(err.error.errors).join(', '));
						},
						complete: () => {
							this.hasPurchased = true;
						}
					})
				}
			})
		}

		this.error = "You do not have enough time capsules to purchase this item."

	}

	onClose(): void {
		this.dialogRef.close();
	}
}
