select
 substr(deslong,15),
x1102sta.wds,
x1102sta.pat,
 DATE_FORMAT('$BDGPC_STARTDATE','%d.%m.%Y'),
  case when
  x1102sta.datf <= '$BDGPC_STARTDATE 23:59:59'
  and x1102sta.datt >=  '$BDGPC_STARTDATE 23:59:58'
  and x8103wds.deslong like 'TimeOfficeKey:%'
  then x1102sta.pat else 0 end
  as nacht,
   case when
  x1102sta.datf <= '$BDGPC_STARTDATE 12:00:01'
  and x1102sta.datt >=  '$BDGPC_STARTDATE 12:00:00'
  and x8103wds.deslong like 'TimeOfficeKey:%'
  then x1102sta.pat else 0 end
  as tag
from x1102sta
 left join x8103wds on x1102sta.wds = x8103wds.wds
 left join x1100pat on x1102sta.pat = x1100pat.pat
where x8103wds.deslong like 'TimeOfficeKey:7%'
 and x1100pat.cha in ('FP','KOMP','S','SGL','T-S','T-SGL')
  and x1102sta.pat not in
  (select pat from x1100pat 
	where admerrord is not NULL or reservmk != 0)
 and 
  x1102sta.datf <= '$BDGPC_STARTDATE 23:59:59'
  and x1102sta.datt >=  '$BDGPC_STARTDATE 00:00:00'
order by deslong
