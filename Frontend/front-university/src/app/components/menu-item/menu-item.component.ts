import { Component, OnInit } from '@angular/core';
import { AppRoutes } from 'src/app/routes/AppRoutes';
import { MenuIcons } from 'src/app/types/MenuIcons';
import { MenuItem } from 'src/app/types/MenuItem.type';
import { PermissionsService } from 'src/app/services/permissions.service';
@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.css']
})
export class MenuItemComponent implements OnInit {


  menuItems:MenuItem[] = [
    {
      name: 'Enrollments',
      icon: MenuIcons.ENROLLMENTS,
      path: AppRoutes.ENROLLMENTS
    },
{
  name: 'Courses',
  icon: MenuIcons.COURSES,
  path: AppRoutes.COURSES
},
{
  name: 'Students',
  icon: MenuIcons.STUDENTS,
  path: AppRoutes.STUDENTS
},
{
  name: 'Categories',
  icon: MenuIcons.CATEGORIES,
  path: AppRoutes.CATEGORIES
},
{
  name: 'Logout',
  icon: MenuIcons.LOGOUT,
  path: AppRoutes.LOGOUT
}


  ];
  constructor(private persmissionService:PermissionsService) { }

  ngOnInit(): void {

     const permise= this.persmissionService.getPermise();

if(!permise){
  this.menuItems=this.menuItems.filter(item=> item.name=='Enrollments'||item.name=='Logout');

  }

  }
}
