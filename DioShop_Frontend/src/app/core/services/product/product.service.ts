import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../api/api.service';
import { Product } from '@models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = '/Product'; // Đường dẫn API của Product

  constructor(private apiService: ApiService) {}

  // ✅ Lấy danh sách sản phẩm
  getAllProducts(): Observable<Product[]> {
    return this.apiService.getTypeRequest<Product[]>(this.baseUrl);
  }

  // ✅ Lấy sản phẩm theo ID
   getProductById(id: number): Observable<Product> {
    return this.apiService.getTypeRequest<Product>(`${this.baseUrl}/${id}`);
  }

  // ✅ Thêm sản phẩm mới
  createProduct(product: Product): Observable<Product> {
    return this.apiService.postTypeRequest<Product>(this.baseUrl, product);
  }

  // ✅ Cập nhật toàn bộ sản phẩm (PUT)
  updateProduct(id: number, product: Product): Observable<Product> {
    return this.apiService.putTypeRequest<Product>(`${this.baseUrl}/${id}`, product);
  }

  // ✅ Cập nhật một phần sản phẩm (PATCH)
  patchProduct(id: number, product: Partial<Product>): Observable<Product> {
    return this.apiService.patchTypeRequest<Product>(`${this.baseUrl}/${id}`, product);
  }

  // ✅ Xóa sản phẩm
  deleteProduct(id: number): Observable<void> {
    return this.apiService.deleteTypeRequest<void>(`${this.baseUrl}/${id}`);
  }
}