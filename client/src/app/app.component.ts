import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';

  constructor(private basketService: BasketService,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadBasket(): void {
    const basketId: string = localStorage.getItem('basket_id');

    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
      }, error => console.log(error));
    }
  }

  loadCurrentUser(): void {
    const token: string = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe(() => {}, error => console.log(error));
  }
}
