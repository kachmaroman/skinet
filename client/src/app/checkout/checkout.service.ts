import { IOrder, IOrderToCreate } from './../shared/models/order';
import { IDeliveryMethod } from './../shared/models/delivery-method';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDeliveryMethods(): Observable<IDeliveryMethod[]> {
    return this.http.get<IDeliveryMethod[]>(this.baseUrl + 'orders/delivery-methods')
      .pipe(map<IDeliveryMethod[], any>((dm: IDeliveryMethod[]) => dm.sort((a, b) => a.price - b.price)));
  }

  createOrder(order: IOrderToCreate): Observable<IOrder> {
    return this.http.post<IOrder>(this.baseUrl + 'orders', order);
  }
}
