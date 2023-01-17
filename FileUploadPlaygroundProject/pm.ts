this.initPm();
this.pageMode = this._activatedRoute.snapshot.data.pageMode;
if ([PageMode.View, PageMode.Edit].includes(this.pageMode)) {
    this.pmId = this._activatedRoute.snapshot.params.id;
    this.getPmById(this.pmId);
    forkJoin([this._pmService.getInitialData(), this._pmService.getPmById(this.pmId)]).subscribe(
        ([initialDto, upsertPmDto]) => {
            if (initialDto) {
                this.initialData = initialDto;
            }
            if (upsertPmDto) {
                this.pmDto = upsertPmDto;
            }
        }
    );
}