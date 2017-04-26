from jchart import Chart
from jchart.config import rgba, DataSet, Axes
from KittyKrawler.models import GameSave, Item
from django.db.models import Max


class AvgTimePerLevelBar(Chart):
    chart_type = 'bar'

    def get_labels(self, *args, **kwargs):
        extend = []
        max_level = GameSave.objects.all().aggregate(Max('next_level'))['next_level__max']
        if max_level > 15:
            extend = ["+"]
            max_level = 14

        return ['Level ' + str(i) for i in range(1,max_level+1)] + extend

    def get_datasets(self, *args, **kwargs):
        calc_level_data = []

        max_level = GameSave.objects.all().aggregate(Max('next_level'))['next_level__max']
        if max_level > 15:
            max_level = 14

        level_data_list = [GameSave.objects.filter(next_level=i) for i in range(1, max_level + 1)]
        level_data_list += [GameSave.objects.filter(next_level__gt=max_level)]

        for i, level_data in enumerate(level_data_list):
            level_time = 0
            for data in level_data:
                level_time += (data.time.days * 24 + data.time.seconds/3600)
            calc_level_data.append(level_time)

        colors = [
            rgba(229, 80, 89, 0.2),
            rgba(229, 80, 161, 0.2),
            rgba(216, 80, 229, 0.2),
            rgba(134, 80, 229, 0.2),
            rgba(80, 102, 229, 0.2),
            rgba(80, 159, 229, 0.2),
            rgba(80, 221, 229, 0.2),
            rgba(92, 229, 80, 0.2),
            rgba(80, 229, 147, 0.2),
            rgba(160, 190, 216, 0.2),
            rgba(201, 229, 80, 0.2),
            rgba(229, 194, 80, 0.2),
            rgba(229, 159, 80, 0.2),
            rgba(229, 119, 80, 0.2),
            rgba(229, 80, 80, 0.2),
        ]

        return [DataSet(
            label='Average Time Per Level (Hours)',
            data=calc_level_data,
            backgroundColor=colors
        )]


class TimeSpentvsLevel(Chart):
    chart_type = 'line'
    scales = {
        'xAxes': [Axes(type='linear', position='bottom')],
        'yAxes': [Axes(type='linear', ticks={'stepSize': 1}, position='left')]
    }

    def get_datasets(self, *args, **kwargs):
        data = []
        level_data = GameSave.objects.all().order_by('time')

        for item in level_data:
            data.append({
                'y': item.next_level,
                'x': round(item.time.seconds/3600, 2)
            })

        return [DataSet(
            type='line',
            label='Time(Hours) Vs Level',
            data=data,
            showLine=False,
            color=(56, 163, 235),
        )]


class PieChartItemsFound(Chart):
    chart_type = 'pie'

    def get_labels(self, *args, **kwargs):
        item_list = list(Item.objects.all().order_by('item_id'))
        return [item.name for item in item_list]

    def get_datasets(self, *args, **kwargs):
        max = Item.objects.all().aggregate(Max('item_id'))['item_id__max'] + 1
        data = [0 for i in range(0, max-1)]
        game_data = GameSave.objects.all()

        for save in game_data:
            for item in save.save_items.all():
                data[int(item.item_id)-1] += 1

        return [DataSet(
            label='Number of times each item has been found',
            data=data,
            backgroundColor=[
                rgba(229, 80, 89, 0.2),
                rgba(229, 80, 161, 0.2),
                rgba(216, 80, 229, 0.2),
                rgba(134, 80, 229, 0.2),
                rgba(80, 102, 229, 0.2),
                rgba(80, 159, 229, 0.2),
                rgba(80, 221, 229, 0.2),
                rgba(92, 229, 80, 0.2),
                rgba(80, 229, 147, 0.2),
                rgba(160, 190, 216, 0.2),
            ]
        )]