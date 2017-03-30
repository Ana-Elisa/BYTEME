from jchart import Chart
from jchart.config import rgba, DataSet, Axes
from KittyKrawler.models import GameSave, Item
from django.db.models import Max


class AvgTimePerLevelBar(Chart):
    chart_type = 'bar'

    def get_labels(self, *args, **kwargs):
        return ['Level 1', 'Level 2', 'Level 3', '+']

    def get_datasets(self, *args, **kwargs):
        data = []

        level0_data = GameSave.objects.filter(next_level=1)
        level1_data = GameSave.objects.filter(next_level=2)
        level2_data = GameSave.objects.filter(next_level=3)
        level3_data = GameSave.objects.filter(next_level__gt=3)

        if len(level0_data) > 0:
            level0_time = 0
            for level_data in level0_data:
                level0_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
            data.append(round(level0_time/len(level0_data), 2))
        else:
            data.append(0)

        if len(level1_data) > 0:
            level1_time = 0
            for level_data in level1_data:
                level1_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
            data.append(round(level1_time/len(level1_data), 2))
        else:
            data.append(0)

        if len(level2_data) > 0:
            level2_time = 0
            for level_data in level2_data:
                level2_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
            data.append(round(level2_time/len(level2_data), 2))
        else:
            data.append(0)

        if len(level3_data) > 0:
            level3_time = 0
            for level_data in level3_data:
                level3_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
            data.append(round(level3_time/len(level3_data), 2))
        else:
            data.append(0)

        colors = [
            rgba(255, 99, 132, 0.2),
            rgba(54, 162, 235, 0.2),
            rgba(255, 206, 86, 0.2),
            rgba(75, 192, 192, 0.2),
        ]

        return [DataSet(
            label='Average Time Per Level (Hours)',
            data=data,
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
        return [item.item_id for item in item_list]

    def get_datasets(self, *args, **kwargs):
        max = Item.objects.all().aggregate(Max('item_id'))['item_id__max'] + 1
        data = [0 for i in range(0, max)]
        game_data = GameSave.objects.all()

        for save in game_data:
            for item in save.save_items.all():
                data[int(item.item_id)] += 1

        data = [item for item in data if item != 0]

        return [DataSet(
            label='Number of times each item has been found',
            data=data
        )]