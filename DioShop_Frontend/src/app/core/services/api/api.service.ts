import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';



@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private baseUrl = environment.baseUrl;
    constructor(private _http: HttpClient) {

    }
    getTypeRequest(url: string) {
        return this._http.get(`${this.baseUrl}${url}`).pipe(
            map((res) => {
                return res;
            })
        );
    }
}
