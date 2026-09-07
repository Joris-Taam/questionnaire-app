import { inject, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RelationService } from '../../shared/services/RelationService.service';
import { ParticipantService } from '../../shared/services/participant.service';
import { Relation, RelationsResponse } from '../../shared/models/relation.model';
import { SaveTargetGroup } from '../../shared/models/SaveTargetGroup.model';

@Component({
  selector: 'app-relation-management',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './relation-management.component.html',
  styleUrls: ['./relation-management.component.scss']
})
export class RelationManagementComponent implements OnInit {
  relationsList: Relation[] = [];
  selectedRelation: Relation | null = null;
  targetGroups: any[] = [];
  administrationId: string | null = null;
  clientCode: string | null = null;
  headerText: string = '';
  headerName: string = '';
  
  #relationService = inject(RelationService);
  #participantService = inject(ParticipantService);
  #route = inject(ActivatedRoute);

  ngOnInit(): void {
    this.#initializeData();
    this.#getAdministrationIdFromURL();
    this.#getAdministrationNameFromURL();
  }

  #initializeData(): void {
    this.clientCode = this.#route.snapshot.queryParamMap.get('id');
    if (this.clientCode) {
      this.#loadRelations(this.clientCode);
    }
  }

  #getAdministrationNameFromURL() {
    this.headerText = this.#route.snapshot.queryParamMap.get('name')!;
  }

  #getAdministrationIdFromURL() {
    this.administrationId = this.#route.snapshot.queryParamMap.get('administration');
  }

  #loadRelations(id: string): void {
    this.#relationService.getData(id).subscribe((data: RelationsResponse) => {
      if (data && Array.isArray(data.results)) {
        this.relationsList = data.results;
        this.headerName = data.results[0].name;
        this.selectedRelation = this.relationsList.length > 0 ? this.relationsList[0] : null;
        this.#loadTargetGroups();
      } else {
        console.error('No valid results found in the response data');
      }
    });
  }

  #loadTargetGroups(): void {
    this.#participantService.getTargetGroups().subscribe(
      (groups: any[]) => {
        this.targetGroups = groups.map((group, index) => ({
          ...group,
          selected: false,
          id: index + 1
        }));
        this.#loadLinkedGroups();
      });
  }

  #loadLinkedGroups(): void {
    if (!this.selectedRelation) {
      return;
    }

    this.#participantService.getParticipantLinkedGroups(this.selectedRelation.contactPerson)
      .subscribe((linkedGroups: number[]) => {
        linkedGroups.forEach(linkedGroupId => {
          const group = this.targetGroups.find(g => g.id === linkedGroupId);
          if (group) {
            group.selected = true;
          }
        });
      });
  }

  protected onCheckboxChange(event: any, group: any): void {
    if (!this.selectedRelation) return;

    group.selected = event.target.checked;

    if (group.selected) {
      this.#saveSelectedGroup(group);
    } else {
      this.#removeSelectedGroup(group);
    }
  }

  #saveSelectedGroup(group: any): void {
    if (!this.selectedRelation) return;

    const dto: SaveTargetGroup = {
      contactPerson: this.selectedRelation.contactPerson,
      email: this.selectedRelation.email,
      targetGroupName: group.target_group_name
    };

    this.#participantService.saveTargetGroup(dto)
      .subscribe({
        next: () => {
          console.log('Target group saved');
        },
        error: () => {
          alert('Failed to save target group');
        }
      });
  }

  #removeSelectedGroup(group: any): void {
    if (!this.selectedRelation) return;
    this.#participantService.removeTargetGroup(this.selectedRelation.contactPerson, group.target_group_name);
  }
}
