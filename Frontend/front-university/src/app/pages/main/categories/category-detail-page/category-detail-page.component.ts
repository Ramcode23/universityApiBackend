import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryCreateDto } from '../../../../services/api/models';

@Component({
  selector: 'app-category-detail-page',
  templateUrl: './category-detail-page.component.html',
  styleUrls: ['./category-detail-page.component.css']
})
export class CategoryDetailPageComponent implements OnInit {
category:CategoryCreateDto|undefined;
  constructor(private _router: Router,
    private activeRouter: ActivatedRoute) { }

  ngOnInit(): void {
    this.category= this.activeRouter.snapshot.params as CategoryCreateDto
    console.log(this.category);
  }

}
