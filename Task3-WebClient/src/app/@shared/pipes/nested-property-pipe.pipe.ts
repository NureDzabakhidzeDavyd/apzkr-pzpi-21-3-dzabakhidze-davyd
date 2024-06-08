import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'nestedProperty'
})
export class NestedPropertyPipe implements PipeTransform {
  transform(value: any, path: string): any {
    if (!value || !path) {
      return null;
    }
    return path.split('.').reduce((acc, part) => acc && acc[part], value);
  }
}
