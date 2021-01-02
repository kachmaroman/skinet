import { IProduct } from './../shared/models/product';
import { Basket, IBasket, IBasketItem, IBasketTotals } from './../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl: string = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket>(null);
  public get basket$(): Observable<IBasket> {
    return this.basketSource.asObservable();
  }

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  public get basketTotal$(): Observable<IBasketTotals> {
    return this.basketTotalSource.asObservable();
  }

  constructor(private http: HttpClient) { }

  getBasket(id: string): Observable<IBasket> {
    return this.http.get<IBasket>(`${this.baseUrl}basket/${id}`)
      .pipe(map<IBasket, any>(basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }));
  }

  setBasket(basket: IBasket): Subscription {
    return this.http.post<IBasket>(`${this.baseUrl}basket`, basket)
      .subscribe(response => {
        this.basketSource.next(response);
        this.calculateTotals();
      }, error => console.log(error));
  }

  addItemToBasket(item: IProduct, quantity: number = 1): void {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
    const basket = this.getOrCreateBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);

    this.setBasket(basket);
  }

  getCurrentBasketValue(): IBasket {
    return this.basketSource.value;
  }

  incrementItemQuantity(item: IBasketItem): void {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    basket.items[foundItemIndex].quantity++;

    this.setBasket(basket);
  }

  decrementItemQuantity(item: IBasketItem): void {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);

    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem): void {
    const basket = this.getCurrentBasketValue();

    if (basket.items.some(x => x.id === item.id)) {
      basket.items = basket.items.filter(i => i.id !== item.id);

      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasket(basket: IBasket): Subscription {
    return this.http.delete(`${this.baseUrl}basket/${basket.id}`).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }

  private calculateTotals() {
    const basket: IBasket = this.getCurrentBasketValue();
    const shipping: number = 0;
    const subtotal: number = basket.items.reduce((prev, next) => prev + (next.price * next.quantity), 0);
    const total: number = shipping  + subtotal;

    this.basketTotalSource.next({shipping, subtotal, total});
  }

  private getOrCreateBasket(): IBasket {
    return this.getCurrentBasketValue() ?? this.createBasket();
  }

  private createBasket(): IBasket {
    const basket = new Basket();

    localStorage.setItem('basket_id', basket.id);

    return basket;
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index: number = items.findIndex(i => i.id === itemToAdd.id);

    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity,
      pictureUrl: item.pictureUrl,
      brand: item.brand,
      type: item.type
    }
  }
}
