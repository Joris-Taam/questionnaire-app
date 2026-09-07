import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {

    private isLocalStorageAvailable(): boolean {
        return typeof window !== 'undefined' && typeof localStorage !== 'undefined';
    }

    public getItems(key: string): any[] {
        if (this.isLocalStorageAvailable()) {
            const items = localStorage.getItem(key);
            if (items) {
                return JSON.parse(items);
            }
        }
        return [];
    }

    public getItemById(key: string, id: string): any | undefined {
        if (this.isLocalStorageAvailable()) {
            const items = localStorage.getItem(key);
            if (items) {
                const tmpArr = JSON.parse(items);
                return tmpArr.find((item: any) => item.id === id);
            }
        }
        return undefined;
    }

    public createItem(key: string, item: any): void {
        if (this.isLocalStorageAvailable()) {
          const items = JSON.parse(localStorage.getItem(key) || '[]');
          items.push(item);
          localStorage.setItem(key, JSON.stringify(items)); 
        }
      }
      

    public editItem(key: string, item: any): void {
        if (this.isLocalStorageAvailable()) {
            const items = localStorage.getItem(key);
            if (items) {
                const tmpArr = JSON.parse(items);
                const index = tmpArr.findIndex((record: any) => record.id === item.id);
                if (index > -1) {
                    tmpArr[index] = item;
                }
                localStorage.setItem(key, JSON.stringify(tmpArr));
            }
        }
    }

    public deleteItem(key: string, id: string): void {
        if (this.isLocalStorageAvailable()) {
            const items = localStorage.getItem(key);
            if (items) {
                const tmpArr = JSON.parse(items);
                const index = tmpArr.findIndex((record: any) => record.id === id);
                if (index > -1) {
                    tmpArr.splice(index, 1);
                }
                localStorage.setItem(key, JSON.stringify(tmpArr));
            }
        }
    }
}
