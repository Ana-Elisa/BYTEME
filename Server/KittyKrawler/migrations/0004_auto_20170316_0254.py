# -*- coding: utf-8 -*-
# Generated by Django 1.10.5 on 2017-03-16 02:54
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0003_auto_20170316_0234'),
    ]

    operations = [
        migrations.AlterField(
            model_name='save',
            name='attack',
            field=models.IntegerField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='defence',
            field=models.IntegerField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='health',
            field=models.IntegerField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='next_level',
            field=models.IntegerField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='speed',
            field=models.IntegerField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='time',
            field=models.TimeField(null=True),
        ),
        migrations.AlterField(
            model_name='save',
            name='total_health',
            field=models.IntegerField(null=True),
        ),
    ]
