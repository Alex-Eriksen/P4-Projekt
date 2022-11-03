import { Component, OnInit } from '@angular/core';
import { DevblogItem } from 'src/app/_models/Misc/devblog-item';
import { FeaturedItem } from 'src/app/_models/Misc/featured-item';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  featuredItems: FeaturedItem[] = [];

  devblogItems: DevblogItem[] = [];

  newItem: FeaturedItem = { itemName: 'Wise Wizard Skin', itemImageLocation: "../../../../assets/featured-item-card.jpg", itemPrice: 375 };

  newDevblog: DevblogItem = { title: "Wizard Battle Version 1.0 Release date", description: "We are not finished. Mf. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Omnis, consectetur repellat.", imageLocation: "../../../assets/devblog-image.jpg"}

  ngOnInit(): void {

    for(let i = 0; i < 4; i++) {
      this.featuredItems.push(this.newItem);
      this.devblogItems.push(this.newDevblog);
    }
  }
}
