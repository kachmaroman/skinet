import { IBasket, IBasketItem } from './../../models/basket';
import { Observable } from 'rxjs';
import { BasketService } from './../../../basket/basket.service';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IOrderItem } from '../../models/order';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent {

  @Input() isBasket: boolean = true;
  @Input() isOrder = false;
  @Input() items: IBasketItem[] | IOrderItem[] = [];
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();

  onDecrementItemQuantity(item: IBasketItem): void {
    this.decrement.emit(item);
  }

  onIncrementItemQuantity(item: IBasketItem): void {
    this.increment.emit(item);
  }

  onRemoveBasketItem(item: IBasketItem): void {
    this.remove.emit(item);
  }
}
