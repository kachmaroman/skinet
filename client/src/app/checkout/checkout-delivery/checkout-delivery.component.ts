import { BasketService } from './../../basket/basket.service';
import { CheckoutService } from './../checkout.service';
import { IDeliveryMethod } from './../../shared/models/delivery-method';
import { FormGroup } from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() checkoutForm: FormGroup;

  deliveryMethods: IDeliveryMethod[];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe((dm: IDeliveryMethod[]) => this.deliveryMethods = dm);;
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod): void {
    this.basketService.setShippingPrice(deliveryMethod);
  }

}
